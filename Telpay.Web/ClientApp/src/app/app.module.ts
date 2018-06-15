import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppComponent } from './app.component';
import { CustomerComponent } from './components/customer/customer.component';
import { BaseHttpService } from './services/basehttp.service';
import { CustomerService } from './services/customer/customer.service';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AuthGuard } from './services/auth-guard.service';
import { AuthComponent } from './components/auth/auth.component';
import { TokenInterceptor } from './services/token.interceptor';
import { HomeComponent } from './components/home/home.component';
import { ConfigurationService } from './services/configuration.service';


@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    AuthComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    OAuthModule.forRoot(),
    FormsModule,
    ModalModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'auth', component: AuthComponent },
      { path: 'customer', component: CustomerComponent, canActivate: [AuthGuard] },
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    ConfigurationService,
    BaseHttpService,
    CustomerService,
    { provide: 'ORIGIN_URL', useFactory: getBaseUrl },
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    }, ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
