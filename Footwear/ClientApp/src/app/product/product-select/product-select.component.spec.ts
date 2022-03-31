import { HttpClient, HttpClientModule } from '@angular/common/http';
import 'rxjs/add/observable/of';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { getBaseUrl } from '../../../environments/environment.test';
import { IProduct } from '../../interfaces/product/product';
import { CartService } from '../../services/cart.service';
import { ProductService } from '../../services/product.service';
import { toastrService } from '../../testing/shared/shared-data';

import { ProductSelectComponent } from './product-select.component';

describe('ProductSelectComponent', () => {
  let component: ProductSelectComponent;
  let fixture: ComponentFixture<ProductSelectComponent>;

  let httpClientSpy: jasmine.SpyObj<HttpClient>;
  let productServiceSpy: ProductService;
  let cartServiceSpy: CartService;

  const expectedResponse = { succeeded: true };
  const testProduct: IProduct = {
    size: 1,
    details: 'test',
    gender: 'male',
    id: 1,
    name: 'test product',
    imageUrl: 'www.sdasdasd.com/img=?s123123123',
    price: 22,
    productType: 'hiking'
  }

  beforeEach(async(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post']);
    productServiceSpy = new ProductService(httpClientSpy, getBaseUrl());
    cartServiceSpy = new CartService(httpClientSpy, getBaseUrl());

    spyOn(productServiceSpy, 'getProductById').and.returnValue(Observable.of(testProduct));
    spyOn(cartServiceSpy, 'addToCart').and.returnValue(Observable.of(expectedResponse));

    TestBed.configureTestingModule({
      declarations: [ProductSelectComponent],
      imports: [HttpClientModule, RouterTestingModule, ToastrModule],
      providers: [
        { provide: ProductService, useValue: productServiceSpy },
        {
          provide: ActivatedRoute, useValue: {
            params: of({
              product: 1,
            }),
          }
        },
        { provide: CartService, useValue: cartServiceSpy },
        RouterTestingModule,
        { provide: ToastrService, useValue: toastrService }
      ]
    })
      .compileComponents();
  }));


  beforeEach(() => {
    fixture = TestBed.createComponent(ProductSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('#addToCart should create', () => {
    expect(component.addToCart).toBeTruthy();
  });

  it('#goBack should create', () => {
    expect(component.goBack).toBeTruthy();
  });

  it('#ngOnInit should create', () => {
    expect(component.ngOnInit).toBeTruthy();
  });

  it('#notFoundHandler should create', () => {
    expect(component.notFoundHandler).toBeTruthy();
  });

  it('#selectedProduct should create', () => {
    expect(component.selectedProduct).toBeTruthy();
  });

  it('#addToCart should function properly', () => {
    cartServiceSpy.addToCart(1, 1).subscribe(data => {
      expect(data).toEqual(expectedResponse);
    })
  });

  it('#ngOnInit to have been called getProductById', () => {
    expect(productServiceSpy.getProductById).toHaveBeenCalled();
  });

});
