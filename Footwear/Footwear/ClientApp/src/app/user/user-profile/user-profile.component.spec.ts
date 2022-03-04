import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { getBaseUrl } from '../../../environments/environment.test';
import { SharedModule } from '../../modules/shared.module';
import { UserService } from '../../services/user.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserProfileComponent } from './user-profile.component';
import { IUserData } from '../../interfaces/user/userData';
import { HttpRequestUserInterceptor } from '../../testing/MockInterceptors/mock-user-interceptor';
import { IEmailData } from '../../interfaces/user/emailData';
import { FormGroupDirective } from '@angular/forms';

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

export const fakeUpdateUserData: IUserData = {
    state: "Pernik",
    street: "koritarova 22",
    city: "Pernik",
    country: "Pernik",
    email: "",
    firstName: "Huski",
    lastName: "Jingibi",
    phone: "293293292333",
    zipCode: "11111"
};

export const fakeEmailData: IEmailData = {
    email: "test@test.test",
    confirmEmail: "test@test.test"
};


describe('UserProfileComponent', () => {

    let component: UserProfileComponent;
    let fixture: ComponentFixture<UserProfileComponent>;
    let testService: UserService;
    let testHttpClient = jasmine.createSpyObj('HttpClient', ['get', 'post', 'delete', 'put']);
    // Mock User service properties
    const baseUrl = getBaseUrl();
    let mockUserService;

    beforeEach(async () => {
        testService = new UserService(testHttpClient, baseUrl);
        /*mockUserService = jasmine.createSpyObj(['updateProfile', 'changeEmail']);*/

        await TestBed.configureTestingModule({
            declarations: [UserProfileComponent],
            imports: [BrowserAnimationsModule, HttpClientTestingModule, SharedModule, ToastrModule.forRoot()],
            providers: [
                {
                    provide: 'BASE_URL',
                    useValue: baseUrl
                },
                ToastrService,
                {
                    provide: UserService,
                    userValue: mockUserService
                },
                {
                    provide: HTTP_INTERCEPTORS,
                    useClass: HttpRequestUserInterceptor,
                    multi: true
                }
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

    it('should #loadDataAsync() on init', async () => {
        const userData = component.userData;
        expect(userData).toEqual(fakeUserData);
    });

});
