import { enableProdMode, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { Router } from '@angular/router';
import { AppModule } from './app/app.module';
import { ConfigurationLoaderService } from './app/services/configuration-loader.service';
import { HealthCheckService } from './app/services/health-check.service';
import { LookupsStore } from './app/store/lookups/lookups.store';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
  if (window) {
    window.console.log = function () {};
  }
}

platformBrowserDynamic().bootstrapModule(AppModule, {
  applicationProviders: [
    provideZoneChangeDetection(),
    provideAppInitializer(async () => {
      const healthCheckService = inject(HealthCheckService);
      const router = inject(Router);
      const configurationLoaderService = inject(ConfigurationLoaderService);
      const lookupsStore = inject(LookupsStore);

      const isHealthy = await healthCheckService.checkHealth();

      if (!isHealthy) {
        router.navigateByUrl('/outage');
        return;
      }

      await Promise.all([
        configurationLoaderService.loadConfiguration(),
        lookupsStore.loadCountries(),
        lookupsStore.loadProvinces(),
        lookupsStore.loadCourts()
      ]);

      const lookupsUnavailable =
        lookupsStore.countriesError() || lookupsStore.provincesError() || lookupsStore.courtsError();

      if (lookupsUnavailable) {
        router.navigateByUrl('/outage');
        return;
      }
    })
  ]
});
