import { useState, type FormEvent } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import { Banner, BrandThemeSwitch, Button, Field, TextInput } from '../components/ui';
import { ApiError } from '../api/http';
import loginBg from '../assets/srisailam-login.jpg';

export function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    if (!username.trim() || !password) {
      setError('Enter a user name and password.');
      return;
    }
    setBusy(true);
    setError(null);
    try {
      await login(username.trim(), password);
      navigate('/', { replace: true });
    } catch (err) {
      setError(err instanceof ApiError ? err.message : 'Unable to sign in.');
    } finally {
      setBusy(false);
    }
  };

  return (
    <div className="login-shell">
      <div className="login-scene-layer photo" style={{ ['--login-photo' as string]: `url(${loginBg})` }} />
      <div className="login-scene-layer classic" />
      <div className="login-theme-dock">
        <BrandThemeSwitch />
      </div>
      <form className="login-card" onSubmit={handleSubmit}>
        <div className="login-glyph">ॐ</div>
        <h1 style={{ textAlign: 'center', fontSize: 19, lineHeight: 1.35, color: 'var(--text)' }}>
          Akhila Bharatha Baktha Markandeya Satram, Srisailam
        </h1>
        <p style={{ textAlign: 'center', color: 'var(--text-muted)', fontSize: 12.5, marginTop: 4, marginBottom: 22 }}>
          Accommodation &amp; Donation Management
        </p>

        {error && <Banner tone="error">{error}</Banner>}

        <Field label="User name">
          <TextInput autoFocus value={username} onChange={(e) => setUsername(e.target.value)} autoComplete="username" />
        </Field>
        <Field label="Password">
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            autoComplete="current-password"
          />
        </Field>

        <Button type="submit" variant="primary" busy={busy} style={{ width: '100%', marginTop: 6 }}>
          Sign in
        </Button>
      </form>
      <div className="login-scene-credit">Srisailam temple entrance — public domain photo (Wikimedia Commons)</div>
    </div>
  );
}
