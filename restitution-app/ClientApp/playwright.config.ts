import { defineConfig, devices } from '@playwright/test';

const isCI = !!process.env['CI'];

export default defineConfig({
  testDir: './e2e/tests',
  timeout: 60_000,
  forbidOnly: isCI,
  retries: isCI ? 2 : 0,
  reporter: [['list'], ['html', { outputFolder: 'playwright-report', open: 'never' }]],
  use: {
    video: 'retain-on-failure',
    screenshot: 'only-on-failure',
    trace: 'on-first-retry',
    viewport: { width: 1280, height: 900 }
  },
  // SPA baseHref is /restwebforms/ (see angular.json); match remote project paths.
  projects: [
    {
      name: 'localhost',
      use: { ...devices['Desktop Chrome'], baseURL: 'http://127.0.0.1:4200/restwebforms/' }
    },
    {
      name: 'dev',
      use: { ...devices['Desktop Chrome'], baseURL: 'https://dev.justice.gov.bc.ca/restwebforms/' }
    },
    {
      name: 'test',
      use: { ...devices['Desktop Chrome'], baseURL: 'https://test.justice.gov.bc.ca/restwebforms/' }
    }
  ],
  webServer: {
    command: 'npm start -- --host 127.0.0.1 --port 4200',
    url: 'http://127.0.0.1:4200/restwebforms/',
    reuseExistingServer: !isCI,
    timeout: 180_000
  }
});
