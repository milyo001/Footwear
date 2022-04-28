import {
  ComponentFixture,
  discardPeriodicTasks,
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
      providers: [UserService, ToastrService],
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
});
