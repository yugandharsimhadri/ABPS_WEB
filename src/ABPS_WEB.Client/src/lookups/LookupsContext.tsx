import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';
import { lookupsApi } from '../api/endpoints';
import type { LookupsResponse } from '../api/types';
import { useAuth } from '../auth/AuthContext';

const LookupsContext = createContext<{ lookups: LookupsResponse | null; loading: boolean; error: string | null }>({
  lookups: null,
  loading: true,
  error: null,
});

export function LookupsProvider({ children }: { children: ReactNode }) {
  const { isAuthenticated } = useAuth();
  const [lookups, setLookups] = useState<LookupsResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!isAuthenticated) {
      setLookups(null);
      return;
    }
    setLoading(true);
    lookupsApi
      .getAll()
      .then(setLookups)
      .catch((err) => setError(err instanceof Error ? err.message : 'Failed to load lookups'))
      .finally(() => setLoading(false));
  }, [isAuthenticated]);

  return <LookupsContext.Provider value={{ lookups, loading, error }}>{children}</LookupsContext.Provider>;
}

export function useLookups() {
  return useContext(LookupsContext);
}
