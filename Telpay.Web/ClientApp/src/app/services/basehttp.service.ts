import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ConfigurationService } from './configuration.service';

@Injectable()
export class BaseHttpService {
  private baseUrl: string;

  constructor(private http: HttpClient, configService: ConfigurationService) {
    this.baseUrl = configService.hostSettings.apiHost;
  }

    public get(url: string): Observable<Object> {
      return this.http.get(this.baseUrl + url);
    }

    public post(url: string, object: any): Observable<Object> {
      return this.http.post(this.baseUrl + url, object);
    }

    public put(url: string, object: any): Observable<Object> {
      return this.http.put(this.baseUrl + url, object);
    }

    public delete(url: string): Observable<Object> {
      return this.http.delete(this.baseUrl + url);
    }
 }
