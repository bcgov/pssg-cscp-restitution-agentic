import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
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
