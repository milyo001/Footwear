import { TestBed } from '@angular/core/testing';
import { ProductService } from './product.service';
describe('ProductService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));
    it('should be created', () => {
        const service = TestBed.get(ProductService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=product.service.spec.js.map