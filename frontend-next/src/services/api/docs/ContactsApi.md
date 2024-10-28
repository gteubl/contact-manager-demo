# ContactManagerDemoRestApi.ContactsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiContactsGet**](ContactsApi.md#apiContactsGet) | **GET** /api/Contacts | 
[**apiContactsPost**](ContactsApi.md#apiContactsPost) | **POST** /api/Contacts | 



## apiContactsGet

> apiContactsGet()



### Example

```javascript
import ContactManagerDemoRestApi from 'contact_manager_demo_rest_api';

let apiInstance = new ContactManagerDemoRestApi.ContactsApi();
apiInstance.apiContactsGet((error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters

This endpoint does not need any parameter.

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined


## apiContactsPost

> apiContactsPost(opts)



### Example

```javascript
import ContactManagerDemoRestApi from 'contact_manager_demo_rest_api';

let apiInstance = new ContactManagerDemoRestApi.ContactsApi();
let opts = {
  'contactDto': new ContactManagerDemoRestApi.ContactDto() // ContactDto | 
};
apiInstance.apiContactsPost(opts, (error, data, response) => {
  if (error) {
    console.error(error);
  } else {
    console.log('API called successfully.');
  }
});
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contactDto** | [**ContactDto**](ContactDto.md)|  | [optional] 

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: application/json, text/json, application/*+json
- **Accept**: Not defined

