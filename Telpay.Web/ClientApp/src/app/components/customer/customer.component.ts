import { Component, OnInit, Inject, TemplateRef } from '@angular/core';
import { Customer } from '../../models/Customer.model';
import { HttpClient } from '@angular/common/http';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ModalOptions } from 'ngx-bootstrap/modal/modal-options.class';
import { CustomerService } from '../../services/customer/customer.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { SaveStatus } from '../../models/saveStatus.model';


@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
      public customers: Customer[];
      modalRef: BsModalRef;
      selectedCustomer: Customer;
      isEditMode: boolean;
      public saveStatus: SaveStatus;

      constructor(private service: CustomerService,
                  private modalService: BsModalService,
                  private oauthService: OAuthService)
      {
          modalService.config.class = 'modal-md';
      }

      ngOnInit(): void {
        this.customers = new Array<Customer>();
        this.saveStatus = new SaveStatus();
         this.getCustomersList();
      }

      public add(template: TemplateRef<any>) {
          this.openModal(template, new Customer());
          this.isEditMode = false;
      }


      public openModal(template: TemplateRef<any>, customer: Customer) {
          this.saveStatus = new SaveStatus();
          this.isEditMode = true;
          this.selectedCustomer = JSON.parse(JSON.stringify(customer));
          this.modalRef = this.modalService.show(template);
      }


      public delete(): void {
        this.service.deleteCustomer(this.selectedCustomer.id).subscribe((res) => {
              this.saveStatus = res as SaveStatus;
              if (this.saveStatus.successful) {
                this.getCustomersList();
              }
            });
      }

      public save(): void {
           if (this.isEditMode) {
             this.service.updateCustomer(this.selectedCustomer).subscribe((res) => {
               this.saveStatus = res as SaveStatus;
               if (this.saveStatus.successful) {
                 this.getCustomersList();
               }
               })
           } else {
             this.service.saveCustomer(this.selectedCustomer).subscribe((res) => {
                  this.saveStatus = res as SaveStatus;
               if (this.saveStatus.successful) {
                 this.getCustomersList();
               }
               });
           }
      }

      public logout() {
        this.oauthService.logOut();
      }

      private getCustomersList() {
           this.service.getCustomers().subscribe((response) => {
           this.customers = response;

          if (this.modalRef != null) {
            this.modalRef.hide();
            this.saveStatus = new SaveStatus();
          }
        });
      }
}
