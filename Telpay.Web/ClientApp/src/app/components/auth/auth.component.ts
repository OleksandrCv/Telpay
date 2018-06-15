import { Component, OnInit } from '@angular/core';
import { OAuthService, AuthConfig } from "angular-oauth2-oidc";
import { Router } from "@angular/router";

@Component({
  template: ''
})
export class AuthComponent implements OnInit {

  constructor(private oauthService: OAuthService, private router: Router) { }

  ngOnInit() {
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
      if (this.oauthService.hasValidIdToken() && this.oauthService.hasValidAccessToken()) {
        this.router.navigate(["/customer"]);
      } else {
        this.router.navigate(["/home"]);
      }
    });
  }
}
