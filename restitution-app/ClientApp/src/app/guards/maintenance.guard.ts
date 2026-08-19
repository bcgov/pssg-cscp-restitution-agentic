import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ConfigurationStore } from '../store/configuration/configuration.store';

/**
 * Redirects to /maintenance when the CONFIGURATION_MAINTENANCE_MODE flag is enabled.
 * The flag is resolved during app initialization (APP_INITIALIZER) before this
 * guard runs, so no async work is needed here.
 */
export const maintenanceGuard: CanActivateFn = () => {
  const configurationStore = inject(ConfigurationStore);
  const router = inject(Router);

  if (configurationStore.maintenanceMode()) {
    return router.createUrlTree(['/maintenance']);
  }

  return true;
};
