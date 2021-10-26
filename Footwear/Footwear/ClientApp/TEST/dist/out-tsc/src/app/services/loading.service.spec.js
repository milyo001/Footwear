import { TestBed } from '@angular/core/testing';
import { LoadingService } from './loading.service';
describe('LoadingService', () => {
    let service;
    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(LoadingService);
    });
    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=loading.service.spec.js.map