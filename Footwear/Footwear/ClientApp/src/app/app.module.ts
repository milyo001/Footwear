import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToastrModule } from 'ngx-toastr';
import { CdkAccordionModule } from '@angular/cdk/accordion';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProductComponent } from './product/product/product.component';
import { AboutComponent } from './about/about.component';
import { ProductSelectComponent } from './product/product-select/product-select.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { FooterComponent } from './footer/footer.component';

import { UserService } from './services/user.service';
import { AuthGuard } from './auth/auth.guard';
import { AuthInterceptor } from './auth/auth.interceptor ';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { UserModule } from './user/user.module';
import { AppRoutingModule } from './modules/router/appRoutingModule';
import { CookieService } from 'ngx-cookie-service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductComponent,
    AboutComponent,
    ProductSelectComponent,
    CartComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({ progressBar: true, positionClass: 'toast-top-left'}),
    NgbModule,
    CdkAccordionModule,
    NgxPaginationModule,
    FontAwesomeModule,
    UserModule
  ],
  providers:[
    UserService,
    CookieService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
