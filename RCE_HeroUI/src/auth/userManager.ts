import { UserManager } from 'oidc-client-ts';
import { oidcConfig } from './oidc-config';

export const userManager = new UserManager(oidcConfig);