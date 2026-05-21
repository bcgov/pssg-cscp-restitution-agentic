import { defineConfig, devices } from '@playwright/test';

const CHROME = { ...devices['Desktop Chrome'], channel: 'chrome' } as const;

export default defineConfig({
  testDir: './e2e/tests',
  timeout: 60_000,
  forbidOnly: !!process.env['CI'],
  retries: process.env['CI'] ? 2 : 0,
  reporter: [['list'], ['html', { outputFolder: 'playwright-report', open: 'never' }]],
  use: {
    video: 'retain-on-failure',
    screenshot: 'only-on-failure',
    trace: 'on-first-retry',
    viewport: { width: 1280, height: 900 }
  },
  projects: [
    {
      name: 'localhost',
      use: { ...CHROME, baseURL: 'http://localhost:4200' }
    },
    {
      name: 'dev',
      use: { ...CHROME, baseURL: 'https://dev.justice.gov.bc.ca/restwebforms/' }
    },
    {
      name: 'test',
      use: { ...CHROME, baseURL: 'https://test.justice.gov.bc.ca/restwebforms/' }
    }
  ]
});
