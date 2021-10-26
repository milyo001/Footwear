import { __awaiter } from "tslib";
import { TestBed } from '@angular/core/testing';
import { UserProfileComponent } from './user-profile.component';
describe('UserProfileComponent', () => {
    let component;
    let fixture;
    beforeEach(() => __awaiter(void 0, void 0, void 0, function* () {
        yield TestBed.configureTestingModule({
            declarations: [UserProfileComponent]
        })
            .compileComponents();
    }));
    beforeEach(() => {
        fixture = TestBed.createComponent(UserProfileComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=user-profile.component.spec.js.map