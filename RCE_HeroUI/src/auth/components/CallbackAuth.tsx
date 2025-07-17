import { userManager } from '@/auth/userManager';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

export default function Callback() {
  const navigate = useNavigate();

  useEffect(() => {
    userManager.signinRedirectCallback().then(() => {
      navigate('/');
    });
  }, [navigate]);

  return <p>Procesando login...</p>;
}