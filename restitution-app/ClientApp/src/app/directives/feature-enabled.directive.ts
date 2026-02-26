import { Directive, ElementRef, Input, OnInit, inject } from '@angular/core';
import { ConfigurationStore } from '../store/configuration/configuration.store';

@Directive({
    selector: '[featureEnabled]',
    standalone: false
})
export class FeatureEnabledDirective implements OnInit {
  @Input('featureEnabled') featureName: string;
  @Input('featureEnabledIf') featureEnabledIf: boolean = true;
  private readonly configurationStore = inject(ConfigurationStore);

  constructor(private el: ElementRef) {}

  ngOnInit() {
    const configuration = this.configurationStore.configuration();
    const isFeatureEnabled = configuration.featureFlags?.[this.featureName];

    if (isFeatureEnabled !== this.featureEnabledIf) {
      this.el.nativeElement.parentNode.removeChild(this.el.nativeElement);
    }
  }
}
