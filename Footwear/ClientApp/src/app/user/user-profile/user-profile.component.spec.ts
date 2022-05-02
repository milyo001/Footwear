import {
  ComponentFixture,
  fakeAsync,
  flush,
  TestBed,
  tick,
} from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../modules/shared.module';
import { UserService } from '../../services/user.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UserProfileComponent } from './user-profile.component';
import { IUserData } from '../../interfaces/user/userData';
import { Observable } from 'rxjs';
import { IEmailData } from 'src/app/interfaces/user/emailData';
import { FormBuilder } from '@angular/forms';
import { IPasswordData } from 'src/app/interfaces/user/passwordData';

// Export the data to use it in a mock interceptor
export const fakeUserData: IUserData = {
  state: 'Sofia',
  street: 'koritarova 22',
  city: 'Toronto',
  country: 'Buglaria',
  email: 'test@test.test',
  firstName: 'Bat Georgi',
  lastName: 'Tomahawka',
  phone: '2231312321',
  zipCode: '22311',
};

describe('UserProfileComponent', () => {
  let component: UserProfileComponent;
  let fixture: ComponentFixture<UserProfileComponent>;
  let userService: UserService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserProfileComponent],
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        SharedModule,
        ToastrModule.forRoot(),
      ],
      providers: [UserService, ToastrService, FormBuilder],
    }).compileComponents();

    fixture = TestBed.createComponent(UserProfileComponent);
    component = fixture.componentInstance;
    userService = fixture.debugElement.injector.get(UserService);
    fixture.detectChanges();
  });

  it('should create Component', () => {
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

  it('#setUserFormValidation() should be created', () => {
    expect(component.setUserFormValidation).toBeTruthy();
  });

  it('#setPasswordFormValidation() should be created', () => {
    expect(component.setPasswordFormValidation).toBeTruthy();
  });

  it('#updateProfile() should be created', () => {
    expect(component.updateProfile).toBeTruthy();
  });

  it('load user data when #loadDataAsync() is called in ngAfterViewInit', fakeAsync(() => {
    spyOn(userService, 'getUserProfile').and.returnValue(
      Promise.resolve(fakeUserData)
    );
    component.loadDataAsync();
    tick(300);
    const userData = component.userData;
    expect(userData).toEqual(fakeUserData);
  }));

  it('#updateUserProfile() updates user profile', fakeAsync(() => {
    spyOn(userService, 'updateUserProfile').and.returnValue(
      Observable.of({ succeeded: true })
    );

    const updatedFakeDataForm : IUserData= {
        state: 'test',
        street: 'koritarova 2222',
        city: 'Veliko Turnovo',
        country: 'Buglaria',
        email: 'test@test.test',
        firstName: 'Bat Georgi',
        lastName: 'Tomahawkata',
        phone: '2231312321',
        zipCode: '22311111',
    };
    // Since the data is passed directly from the form object we need value
    const form = { value: updatedFakeDataForm };

    component.updateProfile(form);
    tick(300);
    const userData = component.userData;
    expect(userData).toEqual(updatedFakeDataForm);
    flush();
  }));

  it('#changeEmail is changing the email of the user as expected', fakeAsync(() => {
    spyOn(userService, 'updateEmail').and.returnValue(
      Observable.of({succeeded:true})
    );
    spyOn(component.emailForm, 'reset').and.callThrough();

    const emailData: IEmailData = {
      email: "test@abv.abv",
      confirmEmail: "test@abv.abv"
    }

    component.ngOnInit();
    component.changeEmail(emailData);
    tick(300);
    const email = component.email;


    expect(email).toEqual(emailData.email);
    expect(component.emailForm.reset).toHaveBeenCalledTimes(1);

    flush();
  }));

  it('#changePassword is changing the password of the user as expected', fakeAsync(() => {
    spyOn(userService, 'updatePassword').and.returnValue(
      Observable.of( { succeeded:true } )
    );
    spyOn(component.passwordForm, 'reset').and.callThrough();
    const passwordData: IPasswordData = {
      password: "UNcrakableeepazzword2244$##$%",
      newPassword: "easierToRemember123",
      confirmPassword: "easierToRemember123"
    }
    component.changePassword(passwordData);
    tick(300);
    expect(component.passwordForm.reset).toHaveBeenCalledTimes(1);

    flush();
  }));

});
