import { enableProdMode, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { ConfigurationLoaderService } from './app/services/configuration-loader.service';
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
    provideAppInitializer(() => inject(ConfigurationLoaderService).loadConfiguration())
  ]
});
