import { Injectable, inject } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ConfigurationService } from '../../api/configuration/configuration.service';
import { Configuration } from '../interfaces/configuration.interface';
import { ConfigurationStore } from '../store/configuration/configuration.store';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationLoaderService {
  private readonly configurationService = inject(ConfigurationService);
  private readonly configurationStore = inject(ConfigurationStore);

  async loadConfiguration(): Promise<void> {
    if (this.configurationStore.isConfigurationLoaded() || this.configurationStore.isConfigurationLoading()) {
      return;
    }

    this.configurationStore.setLoading(true);

    try {
      const configuration = await firstValueFrom(this.configurationService.getApiConfiguration<Configuration>());
      this.configurationStore.setConfiguration(configuration);
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : 'Failed to fetch configuration.';
      this.configurationStore.setError(errorMessage);
    }
  }
}
