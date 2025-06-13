import { generateCodeVerifier, generateCodeChallenge } from './PkceService';

export class AuthService {
  private static readonly AUTH_ENDPOINT = '/auth/connect/authorize';
  private static readonly TOKEN_ENDPOINT = '/auth/connect/token';
  private static readonly CLIENT_ID = 'web-app'; // El ID que configuramos en el backend
  private static readonly REDIRECT_URI = '/callback'; // Tu URL de callback

  public static async initiateLogin() {
    // Generar PKCE
    const codeVerifier = await generateCodeVerifier();
    const codeChallenge = await generateCodeChallenge(codeVerifier);

    // Guardar el code_verifier para usarlo después
    sessionStorage.setItem('code_verifier', codeVerifier);

    // Construir la URL de autorización
    const authUrl = new URL(this.AUTH_ENDPOINT, window.location.origin);
    authUrl.searchParams.append('client_id', this.CLIENT_ID);
    authUrl.searchParams.append('response_type', 'code');
    authUrl.searchParams.append('scope', 'openid profile email api');
    authUrl.searchParams.append('code_challenge', codeChallenge);
    authUrl.searchParams.append('code_challenge_method', 'S256');
    authUrl.searchParams.append('redirect_uri', this.REDIRECT_URI);

    // Redirigir al usuario
    window.location.href = authUrl.toString();
  }

  public static async handleCallback(code: string) {
    const codeVerifier = sessionStorage.getItem('code_verifier');
    if (!codeVerifier) {
      throw new Error('No code verifier found');
    }

    const response = await fetch(this.TOKEN_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: new URLSearchParams({
        client_id: this.CLIENT_ID,
        grant_type: 'authorization_code',
        code: code,
        code_verifier: codeVerifier,
        redirect_uri: this.REDIRECT_URI,
      }),
    });

    if (!response.ok) {
      throw new Error('Failed to exchange code for token');
    }

    const tokens = await response.json();
    // Guardar los tokens
    localStorage.setItem('access_token', tokens.access_token);
    localStorage.setItem('refresh_token', tokens.refresh_token);

    // Limpiar el code_verifier
    sessionStorage.removeItem('code_verifier');

    return tokens;
  }

  public static async refreshToken() {
    const refreshToken = localStorage.getItem('refresh_token');
    if (!refreshToken) {
      throw new Error('No refresh token found');
    }

    const response = await fetch(this.TOKEN_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: new URLSearchParams({
        client_id: this.CLIENT_ID,
        grant_type: 'refresh_token',
        refresh_token: refreshToken,
      }),
    });

    if (!response.ok) {
      throw new Error('Failed to refresh token');
    }

    const tokens = await response.json();
    localStorage.setItem('access_token', tokens.access_token);
    localStorage.setItem('refresh_token', tokens.refresh_token);

    return tokens;
  }

  public static logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    // Redirigir al login
    window.location.href = '/login';
  }
}