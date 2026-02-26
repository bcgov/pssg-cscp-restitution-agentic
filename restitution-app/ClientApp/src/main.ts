import { enableProdMode, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { ConfigurationLoaderService } from './app/services/configuration-loader.service';
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
      await Promise.all([
        inject(ConfigurationLoaderService).loadConfiguration(),
        inject(LookupsStore).loadCountries(),
        inject(LookupsStore).loadProvinces(),
        inject(LookupsStore).loadCities(),
        inject(LookupsStore).loadCourts()
      ]);
    })
  ]
});
