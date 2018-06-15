import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { BaseHttpService } from '../basehttp.service';
import 'rxjs/add/operator/toPromise';

import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { Customer } from '../../models/Customer.model';


@Injectable()
export class CustomerService {
  constructor(private http: BaseHttpService) { }

  public getCustomers() {
      return this.http.get('api/customer/').map(
         res => res as Customer[]
      );
  }

  public saveCustomer(item: Customer) {
      return this.http.post('api/customer/', item).map(res => res);
  }

  public updateCustomer(item: Customer) {
    return this.http.put('api/customer/' + item.id, item).map(res => res);
  }

  public deleteCustomer(customerId: number) {
      return this.http.delete('api/customer/' + customerId).map(res => res);
  }
}
