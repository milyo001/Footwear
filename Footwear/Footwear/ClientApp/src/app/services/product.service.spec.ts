import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { ProductService } from './product.service';

describe('ProductService', () => {

  const service: ProductService = TestBed.get(ProductService);

  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule],
    providers: [{ provide: 'BASE_URL', useValue: 'http://localhost' }]
  }));

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Method getAllProducts should be created', () => {
    expect(service.getAllProducts).toBeTruthy();
  });

  it('Method getProductById should be created', () => {
    expect(service.getProductById).toBeTruthy();
  });

});
