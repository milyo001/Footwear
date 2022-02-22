import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { BrowserModule } from '@angular/platform-browser';
import { getBaseUrl } from '../../environments/environment.test';
import { SharedModule } from '../modules/shared.module';

import { UserService } from './user.service';

describe('UserService', () => {
  let service: UserService;
  const baseUrl = getBaseUrl();

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [BrowserModule, SharedModule, HttpClientModule],
      providers: [{ provide: 'BASE_URL', useValue: baseUrl }]
    });
    service = TestBed.inject(UserService);
  });

  it('Service should be created', () => {
    expect(service).toBeTruthy();
  });

  it('#login should be created', () => {
    expect(service.login).toBeTruthy();
  });

  it('#register should be created', () => {
    expect(service.register).toBeTruthy();
  });
});
