import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthService } from '@/services/Auth/AuthService';

export function Callback() {
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const handleCallback = async () => {
      try {
        const code = new URLSearchParams(window.location.search).get('code');
        if (!code) {
          throw new Error('No code received');
        }

        await AuthService.handleCallback(code);
        navigate('/dashboard'); // O la ruta que prefieras después del login
      } catch (error) {
        setError('Error durante la autenticación');
        console.error('Callback error:', error);
      }
    };

    handleCallback();
  }, [navigate]);

  if (error) {
    return <div className="text-red-500">{error}</div>;
  }

  return <div>Procesando autenticación...</div>;
}