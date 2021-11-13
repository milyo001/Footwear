import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';


const STRIPE_KEY = "pk_test_51JvSm1EzlmwAD2nGbVLotJuNcdiUfqLFluPQ4g6evXT1wdlEI299uJsovNldhvcDM4zrUw17UJBxthwovQm4ZFZA00ZP95L1y6";

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'Stripe_Key', useValue: STRIPE_KEY }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
