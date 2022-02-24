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

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

});
