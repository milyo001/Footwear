import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { ProductService } from './product.service';

describe('ProductService', () => {

  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule],
    providers: [{ provide: 'BASE_URL', useValue: 'http://localhost' }]
  }));

  it('should be created', () => {
    const service: ProductService = TestBed.get(ProductService);
    expect(service).toBeTruthy();
  });

  it('Method getAllProducts should be created', () => {
    const service: ProductService = TestBed.get(ProductService);
    expect(service.getAllProducts).toBeTruthy();
  });

  it('Method getProductById should be created', () => {
    const service: ProductService = TestBed.get(ProductService);
    expect(service.getProductById).toBeTruthy();
  });

  
  

});
