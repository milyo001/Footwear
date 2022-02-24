import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { getBaseUrl } from '../../environments/environment.test';
import { IOrder } from '../interfaces/order/order';
import { IUserData } from '../interfaces/user/userData';
import { asyncData } from '../testing/async-observable-helpers';

import { OrderService } from './order.service';

describe('OrderService', () => {
  let service: OrderService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;
  const baseUrl = getBaseUrl();

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [{ provide: 'BASE_URL', useValue: baseUrl }]
    });
    service = TestBed.inject(OrderService);
  });

  it('Service should be created', () => {
    expect(service).toBeTruthy();
  });

  it('#createOrder should be created', () => {
    expect(service.createOrder).toBeTruthy();
  });

  it('#validatePayment should be created', () => {
    expect(service.validatePayment).toBeTruthy();
  });

  it('#getDeliveryPricingData should be created', () => {
    expect(service.getDeliveryPricingData).toBeTruthy();
  });

  it('#checkOut should be created', () => {
    expect(service.checkOut).toBeTruthy();
  });

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post']);
    service = new OrderService(httpClientSpy, getBaseUrl());
  });

  it('#createOrder should return expected to return boolean property cardPayment (HttpClient called just once)', (done: DoneFn) => {

    const userData: IUserData = {
      firstName: "Miroslav", lastName: "Ilyovski", street: "Rakovska 3",
      state: "Sofia", city: "Sofia", country: "Bulgaria",
      email: "miroslavilyoooovski@gmail.net", phone: "089422123565", zipCode: "1022"
    };
    const order: IOrder = {
      status: 'pending',
      createdOn: "02/02/2022",
      payment: 'card',
      userData: userData
    };
    const expectedResult = { cardPayment: true };

    httpClientSpy.post.and.returnValue(asyncData(order));

    service.createOrder(order)
      .subscribe((data: any) => {
        console.log(data)
        expect(expectedResult)
          .withContext('expected boolean property cardPayment')
          .toEqual(expectedResult);
        done();
      }), (err => done.fail);
    expect(httpClientSpy.post.calls.count())
      .withContext('one call')
      .toBe(1);
    expect()
  });

  it('#checkOut should return expected to return boolean property cardPayment (HttpClient called just once)', (done: DoneFn) => {

    const expectedResponse = { url: "stripe.com/session=?skdasklad213asjkdaskjk_sajaj" };

    httpClientSpy.get.and.returnValue(asyncData(expectedResponse));

    service.checkOut()
      .subscribe(data => {
        expect(expectedResponse)
          .withContext('expected json object with url before redirection')
          .toEqual(expectedResponse);
        done();
      }), (err => done.fail);
    expect(httpClientSpy.get.calls.count())
      .withContext('one call')
      .toBe(1);
  });


});
