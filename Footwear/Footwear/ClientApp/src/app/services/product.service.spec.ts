import { HttpClient, HttpClientModule } from '@angular/common/http';
import { InjectionToken } from '@angular/core';
import {  TestBed } from '@angular/core/testing';
import { getBaseUrl } from '../../main';
import { IProduct } from '../interfaces/product/product';
import { asyncData } from '../testing/async-observable-helpers';
import { ProductService } from './product.service';

describe('ProductService (with spies)', () => {

  let service: ProductService;
  const baseUrl = 'http://localhost';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [{ provide: 'BASE_URL', useValue: baseUrl }]
    });
    service = TestBed.inject(ProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('#getAllProducts should be created', () => {
    expect(service.getAllProducts).toBeTruthy();
  });

  it('#getProductById should be created', () => {
    expect(service.getProductById).toBeTruthy();
  });

});

describe("Test", () => {

  //let service: ProductService;
  //let httpClientSpy: jasmine.SpyObj<HttpClient>;

  //const expectedProducts: IProduct[] =
  //  [{
  //    id: 1, name: "Shoe", size: 22, price: 225.22, details: "Cool shoe", gender: "kids",
  //    imageUrl: "fa.net/img/ss2", productType: "hiking"
  //  },
  //  {
  //    id: 2, name: "Karate Sneakers", size: 42, price: 205.22, details: "Awesome sneakers", gender: "man",
  //    imageUrl: "fa.test/img/ss2", productType: "climbing"
  //    }];
  //const expectedProduct: IProduct = {
  //  id: 1, name: "Shoe", size: 22, price: 225.22, details: "Cool shoe", gender: "kids",
  //  imageUrl: "fa.net/img/ss2", productType: "hiking"
  //};

  //beforeEach(() => {
  //  httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
  //  service = new ProductService(httpClientSpy, getBaseUrl());
  //});

  /* it('should return expected products (HttpClient called just once)', (done: DoneFn) => {*/

    //httpClientSpy.get.and.returnValue(asyncData(expectedProducts));

    //  service.getAllProducts().subscribe({
    //    next: products => {
    //      expect(products)
    //        .withContext('expected products')
    //        .toEqual(expectedProducts);
    //      done();
    //    },
    //    error: done.fail
    //  });
    //  expect(httpClientSpy.get.calls.count())
    //    .withContext('one call')
    //    .toBe(1);

  /*});*/
});
