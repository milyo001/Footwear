import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { getBaseUrl } from '../../environments/environment.test';

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
  
});
