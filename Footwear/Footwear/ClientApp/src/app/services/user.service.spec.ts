import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { BrowserModule } from '@angular/platform-browser';
import { getBaseUrl } from '../../environments/environment.test';
import { SharedModule } from '../modules/shared.module';

import { UserService } from './user.service';

describe('UserService', () => {
  let httpClientSpy: jasmine.SpyObj<HttpClient>;
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

  it('#getUserProfile should be created', () => {
    expect(service.getUserProfile).toBeTruthy();
  });

  it('#updateEmail should be created', () => {
    expect(service.updateEmail).toBeTruthy();
  });

  it('#updatePassword should be created', () => {
    expect(service.updatePassword).toBeTruthy();
  });

  it('#updateUserProfile should be created', () => {
    expect(service.updateUserProfile).toBeTruthy();
  });

  

});
