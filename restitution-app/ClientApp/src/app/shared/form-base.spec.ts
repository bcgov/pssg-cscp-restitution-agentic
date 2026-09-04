import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { FormBase } from './form-base';

describe('FormBase', () => {
  let formBase: FormBase;
  let fb: UntypedFormBuilder;

  beforeEach(() => {
    formBase = new FormBase();
    fb = new UntypedFormBuilder();
  });

  function buildAddressGroup(overrides: any = {}): UntypedFormGroup {
    return fb.group({
      line1: overrides.line1 ?? '',
      line2: overrides.line2 ?? '',
      city: overrides.city ?? '',
      province: overrides.province ?? '',
      country: overrides.country ?? '',
      postalCode: overrides.postalCode ?? ''
    });
  }

  describe('displayMailingSubAddress', () => {
    it('does not render script/markup-like input as live HTML', () => {
      const malicious = '<img src=x onerror=alert(1)>';
      const addressGroup = buildAddressGroup({ line1: malicious, city: 'Victoria' });

      const result = formBase.displayMailingSubAddress(addressGroup);

      expect(result).not.toContain('<img');
      expect(result).toContain('&lt;img src=x onerror=alert(1)&gt;');
    });

    it('still renders normal address values with line breaks', () => {
      const addressGroup = buildAddressGroup({
        line1: '123 Main St',
        city: 'Victoria',
        province: 'BC',
        country: 'Canada',
        postalCode: 'V8V 1V1'
      });

      const result = formBase.displayMailingSubAddress(addressGroup);

      expect(result).toBe('123 Main St<br />Victoria<br />BC<br />Canada<br />V8V 1V1');
    });
  });

  describe('displayMailingAddress', () => {
    it('does not render script/markup-like input as live HTML', () => {
      const malicious = '<img src=x onerror=alert(1)>';
      const addressGroup = buildAddressGroup({ line1: malicious, city: 'Victoria' });

      const result = formBase.displayMailingAddress(addressGroup);

      expect(result).not.toContain('<img');
      expect(result).toContain('&lt;img src=x onerror=alert(1)&gt;');
    });
  });
});
