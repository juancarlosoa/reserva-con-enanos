export const oidcConfig = {
    authority: 'https://localhost:5101/auth',
    client_id: 'react-heroui',
    redirect_uri: 'https://localhost:5173/callback',
    response_type: 'code',
    scope: 'openid profile email api',
    post_logout_redirect_uri: 'https://localhost:5173/',
};