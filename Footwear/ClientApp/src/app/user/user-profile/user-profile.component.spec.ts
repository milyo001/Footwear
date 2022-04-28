import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { getBaseUrl } from '../../../environments/environment.test';
import { SharedModule } from '../../modules/shared.module';
import { UserService } from '../../services/user.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserProfileComponent } from './user-profile.component';
import { IUserData } from '../../interfaces/user/userData';
import { of } from 'rxjs';
import { $ } from 'protractor';

// Export the data to use it in a mock interceptor
export const fakeUserData: IUserData = {
    state: "Sofia",
    street: "koritarova 22",
    city: "Toronto",
    country: "Buglaria",
    email: "test@test.test",
    firstName: "Bat Georgi",
    lastName: "Tomahawka",
    phone: "2231312321",
    zipCode: "22311"
};


describe('UserProfileComponent', () => {

    let component: UserProfileComponent;
    let fixture: ComponentFixture<UserProfileComponent>;
    // Mock User service properties

    beforeEach(async () => {
        // testService = new UserService(testHttpClient);
        /*mockUserService = jasmine.createSpyObj(['updateProfile', 'changeEmail']);*/

        await TestBed.configureTestingModule({
            declarations: [UserProfileComponent],
            imports: [BrowserAnimationsModule, HttpClientTestingModule, SharedModule, ToastrModule.forRoot()],
            providers: [
                {
                  provide: UserService,
                  useValue: {
                  getUserProfile: () => of([fakeUserData])
                  }
                },
                ToastrService,
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(UserProfileComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('Should create Component', () => {
        expect(component).toBeTruthy();
    });

    it('#changeEmail() should create', () => {
        expect(component.changeEmail).toBeTruthy();
    });

    it('#changePassword() should create', () => {
        expect(component.changePassword).toBeTruthy();
    });

    it('#loadDataAsync() should create', () => {
        expect(component.loadDataAsync).toBeTruthy();
    });

    it('#ngAfterViewInit() should be inherited and created', () => {
        expect(component.ngAfterViewInit).toBeTruthy();
    });

    it('#ngOnInit() should be inherited and created', () => {
        expect(component.ngOnInit).toBeTruthy();
    });

    it('#setEmailFormValidation() should be created', () => {
        expect(component.setEmailFormValidation).toBeTruthy();
    });

    it('#setPasswordFormValidation() should be created', () => {
        expect(component.setPasswordFormValidation).toBeTruthy();
    });

    it('#updateProfile() should be created', () => {
        expect(component.updateProfile).toBeTruthy();
    });

    it('should #loadDataAsync() in ngAfterViewInit', fakeAsync( () => {
        const userService = fixture.debugElement.injector.get(UserService);
        spyOn(userService, 'getUserProfile').and.returnValue(Promise.resolve(fakeUserData));
        component.loadDataAsync();
        tick(300);
        const userData = component.userData;
        expect(userData).toEqual(fakeUserData);
    }));


});
