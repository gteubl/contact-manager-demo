/* tslint:disable */
/* eslint-disable */
/**
 * ContactManagerDemo.RestApi
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface ContactsRequest
 */
export interface ContactsRequest {
    /**
     * 
     * @type {number}
     * @memberof ContactsRequest
     */
    skip?: number;
    /**
     * 
     * @type {number}
     * @memberof ContactsRequest
     */
    take?: number;
    /**
     * 
     * @type {string}
     * @memberof ContactsRequest
     */
    orderBy?: string | null;
    /**
     * 
     * @type {boolean}
     * @memberof ContactsRequest
     */
    orderDescending?: boolean;
    /**
     * 
     * @type {Array<string>}
     * @memberof ContactsRequest
     */
    columnsToFilter?: Array<string> | null;
    /**
     * 
     * @type {string}
     * @memberof ContactsRequest
     */
    magicFilter?: string | null;
}

/**
 * Check if a given object implements the ContactsRequest interface.
 */
export function instanceOfContactsRequest(value: object): value is ContactsRequest {
    return true;
}

export function ContactsRequestFromJSON(json: any): ContactsRequest {
    return ContactsRequestFromJSONTyped(json, false);
}

export function ContactsRequestFromJSONTyped(json: any, ignoreDiscriminator: boolean): ContactsRequest {
    if (json == null) {
        return json;
    }
    return {
        
        'skip': json['skip'] == null ? undefined : json['skip'],
        'take': json['take'] == null ? undefined : json['take'],
        'orderBy': json['orderBy'] == null ? undefined : json['orderBy'],
        'orderDescending': json['orderDescending'] == null ? undefined : json['orderDescending'],
        'columnsToFilter': json['columnsToFilter'] == null ? undefined : json['columnsToFilter'],
        'magicFilter': json['magicFilter'] == null ? undefined : json['magicFilter'],
    };
}

  export function ContactsRequestToJSON(json: any): ContactsRequest {
      return ContactsRequestToJSONTyped(json, false);
  }

  export function ContactsRequestToJSONTyped(value?: ContactsRequest | null, ignoreDiscriminator: boolean = false): any {
    if (value == null) {
        return value;
    }

    return {
        
        'skip': value['skip'],
        'take': value['take'],
        'orderBy': value['orderBy'],
        'orderDescending': value['orderDescending'],
        'columnsToFilter': value['columnsToFilter'],
        'magicFilter': value['magicFilter'],
    };
}

