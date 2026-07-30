import { computed } from '@angular/core';
import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import moment from 'moment-timezone';
import { Configuration } from '../../interfaces/configuration.interface';

export interface ConfigurationStoreState {
  configuration: Configuration;
  isConfigurationLoaded: boolean;
  isConfigurationLoading: boolean;
  error: string | null;
}

const initialState: ConfigurationStoreState = {
  configuration: {
    outageStartDate: '',
    outageEndDate: '',
    outageMessage: '',
    featureFlags: {
      useUpdatedComplianceFields: false
    }
  },
  isConfigurationLoaded: false,
  isConfigurationLoading: false,
  error: null
};

export const ConfigurationStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withComputed((store) => ({
    showAnnouncementBanner: computed(() => {
      const { outageMessage: message, outageStartDate: startDate, outageEndDate: endDate } = store.configuration();
      if (!message || !startDate || !endDate) return false;
      const current = moment().tz('America/Vancouver');
      const start = moment(startDate).tz('America/Vancouver');
      const end = moment(endDate).tz('America/Vancouver');
      return current.isBetween(start, end, null, '[]');
    })
  })),
  withMethods((store) => ({
    setLoading(isConfigurationLoading: boolean) {
      patchState(store, { isConfigurationLoading });
    },
    setConfiguration(configuration: Configuration) {
      patchState(store, {
        configuration,
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
