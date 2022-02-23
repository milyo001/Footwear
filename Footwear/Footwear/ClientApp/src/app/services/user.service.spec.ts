import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { BrowserModule } from '@angular/platform-browser';
import { getBaseUrl } from '../../environments/environment.test';
import { IEmailData } from '../interfaces/user/emailData';
import { ILoginData } from '../interfaces/user/loginData';
import { IPasswordData } from '../interfaces/user/passwordData';
import { IRegisterData } from '../interfaces/user/registerData';
import { IUserData } from '../interfaces/user/userData';
import { SharedModule } from '../modules/shared.module';
import { asyncData } from '../testing/async-observable-helpers';

import { UserService } from './user.service';

describe('UserService', () => {
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  let service: UserService;
  const baseUrl = getBaseUrl();

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
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post', 'put']);
    service = new UserService(httpClientSpy, getBaseUrl());
  });

  it('#getUserProfile should return expected user data (HttpClient called just once)', (done: DoneFn) => {

    const userProfileData: IUserData = {
      firstName: "Miroslav", lastName: "Ilyovski", street: "Rakovska 3",
      state: "Sofia", city: "Sofia", country: "Bulgaria",
      email: "miroslavilyovski@gmail.net", phone: "089422123565", zipCode: "1022"
    };
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

  it('#register should return expected status code (HttpClient called just once)', (done: DoneFn) => {

    var expectedResponse = { succeeded: true };
    const fakeRegisterData: IRegisterData = {
      email: "rare@email.test",
      password: "123456789_10",
      firstName:"Johny",
      lastName: "Bravo",
      phone: "21312331555"
    }

    httpClientSpy.post.and.returnValue(asyncData(expectedResponse));

    service.register(fakeRegisterData)
      .subscribe(data => {
        expect(expectedResponse)
          .withContext('expected succeeded property')
          .toEqual(expectedResponse);
        done();
      }), (err => done.fail());

    expect(httpClientSpy.post.calls.count())
      .withContext('one call')
      .toBe(1);
  });

  it('#login should return expected auth token (HttpClient called just once)', (done: DoneFn) => {

    const expectedResponse = { token: "testtestesttesttest" };
    const fakeLoginData: ILoginData = {
      email: "rare@email.test",
      password: "123456789_10",
    };

    httpClientSpy.post.and.returnValue(asyncData(expectedResponse));

    service.login(fakeLoginData)
      .subscribe(data => {
        expect(expectedResponse)
          .withContext('expected token property')
          .toEqual(expectedResponse);
        done();
      }), (err => done.fail());

    expect(httpClientSpy.post.calls.count())
      .withContext('one call')
      .toBe(1);
  });

  it('#updateEmail should return expected auth token (HttpClient called just once)', (done: DoneFn) => {

    const expectedResponse = { succeeded: true };
    const fakeLoginData: IEmailData = {
      email: "rare@email.test",
      confirmEmail: "rate@email.test"
    };

    httpClientSpy.put.and.returnValue(asyncData(expectedResponse));

    service.updateEmail(fakeLoginData)
      .subscribe(data => {
        expect(expectedResponse)
          .withContext('expected succeeded property')
          .toEqual(expectedResponse);
        done();
      }), (err => done.fail());

    expect(httpClientSpy.put.calls.count())
      .withContext('one call')
      .toBe(1);
  });

  it('#updatePassword should return expected auth token (HttpClient called just once)', (done: DoneFn) => {

    const expectedResponse = { succeeded: true };
    const fakePassData: IPasswordData = {
      password: "trytobullforceME22$@!",
      confirmPassword: "trytobullforceME22$@!",
      newPassword: "trytobullforceME22$@!"
    };

    httpClientSpy.put.and.returnValue(asyncData(expectedResponse));

    service.updatePassword(fakePassData)
      .subscribe(data => {
        expect(expectedResponse)
          .withContext('expected succeeded property')
          .toEqual(expectedResponse);
        done();
      }), (err => done.fail());

    expect(httpClientSpy.put.calls.count())
      .withContext('one call')
      .toBe(1);
  });

  it('#updateUserProfile should return expected succeeded property (HttpClient called just once)', (done: DoneFn) => {

    const expectedResponse = { succeeded: true };
    const fakeUserData: IUserData = {
      email: "test@yahoo.com",
      city: "Blagoevgrad",
      country: "Bulgaria",
      firstName: "Nasakoto",
      lastName: "Qkata",
      phone: "089437729",
      state: "Blagoevgrad",
      street: "ul Lenovo  37A",
      zipCode: "2700"
    };

    httpClientSpy.put.and.returnValue(asyncData(expectedResponse));

    service.updateUserProfile(fakeUserData)
      .subscribe(data => {
        expect(expectedResponse)
          .withContext('expected succeeded property')
          .toEqual(expectedResponse);
        done();
      }), (err => done.fail());

    expect(httpClientSpy.put.calls.count())
      .withContext('one call')
      .toBe(1);
  });
});

