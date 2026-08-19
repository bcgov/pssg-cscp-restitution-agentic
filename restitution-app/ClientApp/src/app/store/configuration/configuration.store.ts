import { computed } from '@angular/core';
import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import { Configuration, FeatureFlagConfiguration } from '../../interfaces/configuration.interface';

export interface ConfigurationStoreState {
  outageStartDate: string;
  outageEndDate: string;
  outageMessage: string;
  maintenanceMode: boolean;
  featureFlags: FeatureFlagConfiguration;
  isConfigurationLoaded: boolean;
  isConfigurationLoading: boolean;
  error: string | null;
}

const initialState: ConfigurationStoreState = {
  outageStartDate: '',
  outageEndDate: '',
  outageMessage: '',
  maintenanceMode: false,
  featureFlags: { useUpdatedComplianceFields: false },
  isConfigurationLoaded: false,
  isConfigurationLoading: false,
  error: null
};

export const ConfigurationStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withComputed((store) => ({
    // Reconstructed for backward-compatibility with consumers that still call store.configuration().
    configuration: computed<Configuration>(() => ({
      outageStartDate: store.outageStartDate(),
      outageEndDate: store.outageEndDate(),
      outageMessage: store.outageMessage(),
      maintenanceMode: store.maintenanceMode(),
      featureFlags: store.featureFlags()
    })),
    showAnnouncementBanner: computed(() => {
      const message = store.outageMessage();
      const startDate = store.outageStartDate();
      const endDate = store.outageEndDate();
      if (!message || !startDate || !endDate) return false;
      // Dates are ISO 8601 UTC strings (e.g. "2025-09-25T04:00:00Z"). Compare as UTC epoch
      // values so the banner appears at the same instant for all users regardless of locale.
      const now = Date.now();
      return now >= new Date(startDate).getTime() && now <= new Date(endDate).getTime();
    })
  })),
  withMethods((store) => ({
    setLoading(isConfigurationLoading: boolean) {
      patchState(store, { isConfigurationLoading });
    },
    setConfiguration(configuration: Configuration) {
      patchState(store, {
        outageStartDate: configuration.outageStartDate ?? '',
        outageEndDate: configuration.outageEndDate ?? '',
        outageMessage: configuration.outageMessage ?? '',
        maintenanceMode: configuration.maintenanceMode ?? false,
        featureFlags: configuration.featureFlags ?? { useUpdatedComplianceFields: false },
        isConfigurationLoaded: true,
        isConfigurationLoading: false,
        error: null
      });
    },
    setError(error: string) {
      patchState(store, {
        error,
        isConfigurationLoading: false
      });
    }
  }))
);
