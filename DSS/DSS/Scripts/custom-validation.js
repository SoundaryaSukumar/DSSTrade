$(document).ready(function () {
$("form[name='site_info']").validate({
    rules: {    
      site_title: "required",     
	  website_team: "required",
	  site_url: "required",     
	  keyword: "required",
	  descript: "required",
	  currency: "required"
    },
   /*  messages: {
      site_title: "This field is required",
	  website_team: "This field is required",
	  site_url: "This field is required",
	  keyword: "This field is required",
	  descript: "This field is required"
    }, */
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='site_contact_info']").validate({
    rules: {    
      site_email: "required",     
	  website_contact: "required",
	  website_addr: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='cms_info']").validate({
    rules: {    
      about_us: "required",     
	  terms: "required",
	  privacy_policy: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='advance_settings']").validate({
    rules: {    
      id_prefix: "required",
	  epin_sts: "required",
	  ref_sts: "required",
	  lvl_sts: "required",
	  roi: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='smtp_settings']").validate({
    rules: {    
      host_name: "required",
	  port: "required",
	  username: "required",
	  password: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='social_lg']").validate({
    rules: {    
      fb_idv: "required",
	  fb_skey: "required",
	  fb_redir: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='membership']").validate({
    rules: {    
      pack: "required",
	  amount: "required",
	  duration: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='epinfrm']").validate({
    rules: {    
      mem_pack: "required",
	  count: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='spnsrfrm']").validate({
    rules: {
	  sponsor_name: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='newsfrm']").validate({
    rules: {
	  sponsor_name: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='sliderfrm']").validate({
    rules: {
	  sponsor_name: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='eventfrm']").validate({
    rules: {
	  sponsor_name: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='bnk_settings']").validate({
    rules: {    
      acc_name: "required",
	  acc_bank: "required",
	  branch: "required",
	  acct_no: "required",
	  ifsc: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='admusrfrm']").validate({
    rules: {
	  first_name: "required",
	  phone_no: "required",
	  usr_dob: "required",
	  country_name: "required",
	  postcode: "required",
	  n_name: "required",
	  n_email: "required",
	  n_phone: "required",
	  ndob: "required",
	  nid_proof: "required",
	  nid_proof_no: "required",
	  ncountry_name: "required",
	  npostcode: "required",
    },
    submitHandler: function(form) {
      form.submit();
    }
});

//User side
$("form[name='regfrm']").validate({
    rules: {
	  sponsor_id: "required",
	  fname: "required",
	  email: "required",
	  phone: "required",
	  pass: "required",
	  cpass: {
        required: true,
		equalTo: "#passId"
      }
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='logfrm']").validate({
    rules: {
	  username: "required",
	  password: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='usrbnkfrm']").validate({
    rules: {
	  bank_name: "required",
	  branch: "required",
	  acc_no: "required",
	  ifsc: "required",
	  acc_holder: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='usrprffrm']").validate({
    rules: {
	  fname: "required",
	  phone_no: "required",
	  usr_dob: "required",
	  country_name: "required",
	  postcode: "required",
	  n_name: "required",
	  n_email: "required",
	  n_phone: "required",
	  ndob: "required",
	  nid_proof: "required",
	  nid_proof_no: "required",
	  ncountry_name: "required",
	  npostcode: "required",
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='passchngfrm']").validate({
    rules: {
	  cur_pass: "required",
	  new_pass: "required",
	  cpass: {
        required: true,
		equalTo: "#newpassId"
      }
    },
    submitHandler: function(form) {
      form.submit();
    }
});

$("form[name='withdraw']").validate({
    rules: {
	  withdraw_amt: "required",
	  sub: "required",
	  reason: "required"
    },
    submitHandler: function(form) {
      form.submit();
    }
});
});