import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HostSettings } from '../models/host-settings.model';

@Injectable()
export class ConfigurationService {
  private originURL: string;
  public hostSettings: HostSettings;

  constructor(private http: HttpClient, @Inject('ORIGIN_URL') originURL) {
    this.originURL = originURL;
  }

  init(): Observable<HostSettings> {
    return this.http.get(this.originURL + 'api/configuration/hostsettings').map((response) => {
      this.hostSettings = response as HostSettings;
      return this.hostSettings;
    });
  }
 }
