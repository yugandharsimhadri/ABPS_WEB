const BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5297/api';

export class ApiError extends Error {
  status: number;
  constructor(status: number, message: string) {
    super(message);
    this.status = status;
  }
}

let token: string | null = localStorage.getItem('abps.token');
let onUnauthorized: (() => void) | null = null;

export function setToken(next: string | null) {
  token = next;
  if (next) localStorage.setItem('abps.token', next);
  else localStorage.removeItem('abps.token');
}

export function getToken() {
  return token;
}

export function onAuthExpired(handler: () => void) {
  onUnauthorized = handler;
}

function buildQuery(params?: Record<string, unknown>): string {
  if (!params) return '';
  const usp = new URLSearchParams();
  for (const [key, value] of Object.entries(params)) {
    if (value === undefined || value === null || value === '') continue;
    usp.set(key, String(value));
  }
  const qs = usp.toString();
  return qs ? `?${qs}` : '';
}

interface RequestOptions {
  method?: 'GET' | 'POST' | 'PUT' | 'DELETE';
  body?: unknown;
  query?: Record<string, unknown>;
  isForm?: boolean;
}

async function request<T>(path: string, options: RequestOptions = {}): Promise<T> {
  const { method = 'GET', body, query, isForm } = options;
  const headers: Record<string, string> = {};
  const hadToken = !!token;
  if (token) headers['Authorization'] = `Bearer ${token}`;
  if (!isForm && body !== undefined) headers['Content-Type'] = 'application/json';

  let response: Response;
  try {
    response = await fetch(`${BASE_URL}${path}${buildQuery(query)}`, {
      method,
      headers,
      body: isForm ? (body as FormData) : body !== undefined ? JSON.stringify(body) : undefined,
    });
  } catch {
    throw new ApiError(0, 'Cannot reach the API server. Confirm the backend is running.');
  }

  // A 401 only means "your session expired" when this request actually carried a token
  // (e.g. an authenticated screen's data call). The login endpoint itself returns 401 for
  // wrong credentials on a request with no token at all — that must surface the server's
  // real message ("Invalid user name or password."), not a stale-session message.
  if (response.status === 401 && hadToken) {
    onUnauthorized?.();
    throw new ApiError(401, 'Session expired. Please sign in again.');
  }

  if (!response.ok) {
    let message = `Request failed (${response.status})`;
    try {
      const data = await response.json();
      message = data?.message ?? message;
    } catch {
      // non-JSON error body, keep default message
    }
    throw new ApiError(response.status, message);
  }

  if (response.status === 204) return undefined as T;
  const text = await response.text();
  if (!text) return undefined as T;
  return JSON.parse(text) as T;
}

export const http = {
  get: <T>(path: string, query?: Record<string, unknown>) => request<T>(path, { method: 'GET', query }),
  post: <T>(path: string, body?: unknown, query?: Record<string, unknown>) => request<T>(path, { method: 'POST', body, query }),
  put: <T>(path: string, body?: unknown, query?: Record<string, unknown>) => request<T>(path, { method: 'PUT', body, query }),
  del: <T>(path: string, query?: Record<string, unknown>) => request<T>(path, { method: 'DELETE', query }),
  postForm: <T>(path: string, form: FormData) => request<T>(path, { method: 'POST', body: form, isForm: true }),
};
