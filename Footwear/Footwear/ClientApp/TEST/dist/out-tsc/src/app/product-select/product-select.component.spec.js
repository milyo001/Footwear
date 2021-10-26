import { async, TestBed } from '@angular/core/testing';
import { ProductSelectComponent } from './product-select.component';
describe('ProductSelectComponent', () => {
    let component;
    let fixture;
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ProductSelectComponent]
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
});
//# sourceMappingURL=product-select.component.spec.js.map