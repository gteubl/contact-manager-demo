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
import type { Gender } from './Gender';
import {
    GenderFromJSON,
    GenderFromJSONTyped,
    GenderToJSON,
    GenderToJSONTyped,
} from './Gender';
import type { CityDto } from './CityDto';
import {
    CityDtoFromJSON,
    CityDtoFromJSONTyped,
    CityDtoToJSON,
    CityDtoToJSONTyped,
} from './CityDto';

/**
 * 
 * @export
 * @interface ContactDto
 */
export interface ContactDto {
    /**
     * 
     * @type {string}
     * @memberof ContactDto
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof ContactDto
     */
    firstName: string | null;
    /**
     * 
     * @type {string}
     * @memberof ContactDto
     */
    lastName: string | null;
    /**
     * 
     * @type {Gender}
     * @memberof ContactDto
     */
    gender: Gender;
    /**
     * 
     * @type {string}
     * @memberof ContactDto
     */
    email: string | null;
    /**
     * 
     * @type {Date}
     * @memberof ContactDto
     */
    birthDate?: Date | null;
    /**
     * 
     * @type {string}
     * @memberof ContactDto
     */
    phoneNumber?: string | null;
    /**
     * 
     * @type {CityDto}
     * @memberof ContactDto
     */
    city?: CityDto;
}



/**
 * Check if a given object implements the ContactDto interface.
 */
export function instanceOfContactDto(value: object): value is ContactDto {
    if (!('firstName' in value) || value['firstName'] === undefined) return false;
    if (!('lastName' in value) || value['lastName'] === undefined) return false;
    if (!('gender' in value) || value['gender'] === undefined) return false;
    if (!('email' in value) || value['email'] === undefined) return false;
    return true;
}

export function ContactDtoFromJSON(json: any): ContactDto {
    return ContactDtoFromJSONTyped(json, false);
}

export function ContactDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): ContactDto {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'] == null ? undefined : json['id'],
        'firstName': json['firstName'],
        'lastName': json['lastName'],
        'gender': GenderFromJSON(json['gender']),
        'email': json['email'],
        'birthDate': json['birthDate'] == null ? undefined : (new Date(json['birthDate'])),
        'phoneNumber': json['phoneNumber'] == null ? undefined : json['phoneNumber'],
        'city': json['city'] == null ? undefined : CityDtoFromJSON(json['city']),
    };
}

  export function ContactDtoToJSON(json: any): ContactDto {
      return ContactDtoToJSONTyped(json, false);
  }

  export function ContactDtoToJSONTyped(value?: ContactDto | null, ignoreDiscriminator: boolean = false): any {
    if (value == null) {
        return value;
    }

    return {
        
        'id': value['id'],
        'firstName': value['firstName'],
        'lastName': value['lastName'],
        'gender': GenderToJSON(value['gender']),
        'email': value['email'],
        'birthDate': value['birthDate'] == null ? undefined : ((value['birthDate'] as any).toISOString()),
        'phoneNumber': value['phoneNumber'],
        'city': CityDtoToJSON(value['city']),
    };
}

