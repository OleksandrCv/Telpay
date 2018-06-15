import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { AuthConfig } from "angular-oauth2-oidc";
import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { ConfigurationService } from './services/configuration.service';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  loading: boolean = true;

  constructor(private router: Router, @Inject(PLATFORM_ID) private platformId: Object, private oauthService: OAuthService,
    @Inject('ORIGIN_URL') originURL, configService: ConfigurationService) {
    if (isPlatformBrowser(this.platformId)) {

      configService.init().subscribe(() => {
        let authConfig: AuthConfig = {
          requireHttps: true,
          clientId: "implicit",
          scope: "openid profile email api",
          issuer: configService.hostSettings.authHost,
          redirectUri: originURL + 'auth/',
          silentRefreshRedirectUri: originURL + '/silent-refresh.html',
          logoutUrl: originURL
        };

        this.oauthService.configure(authConfig);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
        this.oauthService.loadDiscoveryDocumentAndTryLogin();
        this.loading = false;
      });
    }
  }
}
