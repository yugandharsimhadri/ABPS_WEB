import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';

export type BrandTheme = 'srisailam' | 'classic';
export type ThemeMode = 'day' | 'dusk';

const BRAND_STORAGE_KEY = 'abps.brandTheme';
const MODE_STORAGE_KEY = 'abps.themeMode';

interface BrandThemeContextValue {
  brand: BrandTheme;
  setBrand: (brand: BrandTheme) => void;
  mode: ThemeMode;
  setMode: (mode: ThemeMode) => void;
}

const BrandThemeContext = createContext<BrandThemeContextValue | null>(null);

function readStoredBrand(): BrandTheme {
  const stored = localStorage.getItem(BRAND_STORAGE_KEY);
  return stored === 'classic' ? 'classic' : 'srisailam';
}

function readStoredMode(): ThemeMode {
  const stored = localStorage.getItem(MODE_STORAGE_KEY);
  return stored === 'dusk' ? 'dusk' : 'day';
}

export function BrandThemeProvider({ children }: { children: ReactNode }) {
  const [brand, setBrand] = useState<BrandTheme>(readStoredBrand);
  const [mode, setMode] = useState<ThemeMode>(readStoredMode);

  useEffect(() => {
    document.documentElement.setAttribute('data-brand', brand);
    localStorage.setItem(BRAND_STORAGE_KEY, brand);
  }, [brand]);

  useEffect(() => {
    // Only stamp the attribute in dusk mode so day mode needs no CSS override at all.
    if (mode === 'dusk') document.documentElement.setAttribute('data-theme', 'dusk');
    else document.documentElement.removeAttribute('data-theme');
    localStorage.setItem(MODE_STORAGE_KEY, mode);
  }, [mode]);

  return (
    <BrandThemeContext.Provider value={{ brand, setBrand, mode, setMode }}>{children}</BrandThemeContext.Provider>
  );
}

export function useBrandTheme() {
  const ctx = useContext(BrandThemeContext);
  if (!ctx) throw new Error('useBrandTheme must be used within BrandThemeProvider');
  return ctx;
}
