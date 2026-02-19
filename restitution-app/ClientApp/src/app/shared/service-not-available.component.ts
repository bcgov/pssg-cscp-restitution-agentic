import { Component } from '@angular/core';

@Component({
    selector: 'app-service-not-available',
    template: `
    <div class="service-error-message">
      <p>The Restitution application is currently down. Please retry later or contact the Restitution Unit:</p>
      <p>
        Telephone: 604-660-4898<br />
        Toll Free: 1-844-660-4898<br />
        Email: <a href="mailto:restitution@gov.bc.ca">restitution&#64;gov.bc.ca</a>
      </p>
    </div>
  `,
    styles: [
        `
      .service-error-message {
        color: white;
      }
      .service-error-message p {
        margin: 8px 0;
      }
      .service-error-message a {
        color: white;
        text-decoration: underline;
      }
    `
    ],
    standalone: false
})
export class ServiceNotAvailableComponent {
  constructor() {}
}
