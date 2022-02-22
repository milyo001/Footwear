import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { BrowserModule } from '@angular/platform-browser';
import { getBaseUrl } from '../../environments/environment.test';
import { IUserData } from '../interfaces/user/userData';
import { SharedModule } from '../modules/shared.module';
import { asyncData } from '../testing/async-observable-helpers';

import { UserService } from './user.service';

describe('UserService', () => {
  let httpClientSpy: jasmine.SpyObj<HttpClient>;
  let service: UserService;
  const baseUrl = getBaseUrl();

  const userProfileData: IUserData = {
    firstName: "Miroslav", lastName: "Ilyovski", street: "Rakovska 3",
    state: "Sofia", city: "Sofia", country: "Bulgaria",
    email: "miroslavilyovski@gmail.net", phone: "089422123565", zipCode: "1022"
  };

  //Test if methods are created
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

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
    service = new UserService(httpClientSpy, getBaseUrl());
  });

  it('#getUserProfile should return expected user data (HttpClient called just once)', (done: DoneFn) => {

    httpClientSpy.get.and.returnValue(asyncData(userProfileData));

    service.getUserProfile()
      .then(data => {
        expect(userProfileData)
          .withContext('expected user data')
          .toEqual(userProfileData);
        done();
      })
      .catch(err => done.fail);
    expect(httpClientSpy.get.calls.count())
      .withContext('one call')
      .toBe(1);
  });

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['post']);
    service = new UserService(httpClientSpy, getBaseUrl());
  });


});
