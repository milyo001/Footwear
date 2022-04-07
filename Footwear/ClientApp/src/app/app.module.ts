import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AboutComponent } from './about/about.component';
import { CartComponent } from './cart/cart.component';
import { FooterComponent } from './footer/footer.component';

import { UserService } from './services/user.service';
import { AuthInterceptor } from './interceptors/auth.interceptor ';
import { AppRoutingModule } from './modules/routing.module';
import { CookieService } from 'ngx-cookie-service';
import { ModalComponent } from './cart/modal.component';
import { LoadingService } from './services/loading.service';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { PaymentSuccessComponent } from './payment/payment-success/payment-success.component';
import { PaymentCancelComponent } from './payment/payment-cancel/payment-cancel.component';
import { OrderService } from './services/order.service';
import { SharedModule } from './modules/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AboutComponent,
    CartComponent,
    FooterComponent,
    ModalComponent,
    PaymentSuccessComponent,
    PaymentCancelComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    ToastrModule.forRoot({ progressBar: true, positionClass: 'toast-top-left' }),
    NgbModule,
    SharedModule
  ],
  providers:[
    UserService,
    CookieService,
    LoadingService,
    OrderService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, //For validating if user is logged in.
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }, //For showing mat-progress-spinner when fetching data.
  ],
  bootstrap: [AppComponent],
  entryComponents: [ModalComponent]
})
export class AppModule { }
