import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { getBaseUrl } from '../../environments/environment.test';
import { ICartProduct } from '../interfaces/cart/cartProduct';

import { CartService } from './cart.service';

describe('CartService', () => {
  let service: CartService;
  const baseUrl = getBaseUrl();
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule],
    providers: [{ provide: 'BASE_URL', useValue: baseUrl }]
  }));

  it('Service should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service).toBeTruthy();
  });

  it('#addToCart should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.addToCart).toBeTruthy();
  });

  it('#defaultQuantity should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.defaultQuantity).toBeTruthy();
  });

  it('#decreaseProductQuantity should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.decreaseProductQuantity).toBeTruthy();
  });

  it('#increaseProductQuantity should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.increaseProductQuantity).toBeTruthy();
  });

  it('#deleteCartProduct should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.deleteCartProduct).toBeTruthy();
  });

  it('#getAllCartProducts should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.getAllCartProducts).toBeTruthy();
  });

  it('#removeAllCartProduts should be created', () => {
    const service: CartService = TestBed.get(CartService);
    expect(service.removeAllCartProduts).toBeTruthy();
  });

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post', 'put', 'delete']);
    service = new CartService(httpClientSpy, getBaseUrl());
  });

  it('#getAllProducts should return expected products (HttpClient called just once)', (done: DoneFn) => {

    //const expectedBody: ICartProduct = {
    //  productId: 1,
    //  name: "Product",
    //  size: product.size,
    //  details: product.details,
    //  imageUrl: product.imageUrl,
    //  gender: product.gender,
    //  productType: product.productType,
    //  price: product.price,
    //  quantity: this.defaultQuantity
    //}

    httpClientSpy.get.and.returnValue(asyncData(expectedProducts));

    service.getAllProducts().subscribe({
      next: products => {
        expect(products)
          .withContext('expected products')
          .toEqual(expectedProducts);
        done();
      },
      error: done.fail
    });
    expect(httpClientSpy.get.calls.count())
      .withContext('one call')
      .toBe(1);
  });
});
