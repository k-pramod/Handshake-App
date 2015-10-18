package com.handshake.tommaso.handshake;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import org.apache.commons.io.IOUtils;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class MainActivity extends ActionBarActivity {

    EditText email;
    EditText pass;
    Button login;
    Bundle bundle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        StrictMode.enableDefaults();
        setContentView(R.layout.activity_main);
        email = (EditText) findViewById(R.id.editText);
        pass = (EditText) findViewById(R.id.editText2);
        login = (Button) findViewById(R.id.button);
        login.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v) {
                String emailData = email.getText().toString();
                String passData = pass.getText().toString();
                List<NameValuePair> data = new ArrayList<NameValuePair>();
                data.add(new BasicNameValuePair("email",emailData));
                data.add(new BasicNameValuePair("password",passData));
                sendData(data);
            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    private void sendData(List<NameValuePair> data) {
        InputStream is;
        try{
            Log.e("log1","first connection try");
            HttpClient httpclient = new DefaultHttpClient();
            HttpPost httppost = new HttpPost("http://handshake.azurewebsites.net/Users/Login");
            httppost.setEntity(new UrlEncodedFormEntity(data));
            HttpResponse httpResponse = httpclient.execute(httppost);
            HttpEntity entity = httpResponse.getEntity();
            is = entity.getContent();
            StringWriter writer = new StringWriter();
            IOUtils.copy(is, writer, "UTF-8");
            String theString = writer.toString();
            String[] response = theString.split(",");
            Integer ID = Integer.parseInt(response[0]);
            String userType = response[1];
            String id = Integer.toString(ID);
            InputStream is2;
            Log.e("log_id", id);
            is.close();
            Log.e("log1", "first connection done, into recruiter");
            List<NameValuePair> data2 = new ArrayList<>();
            data2.add(new BasicNameValuePair("userId", id));
            HttpClient httpclient2 = new DefaultHttpClient();
            HttpPost httppost2 = new HttpPost("http://handshake.azurewebsites.net/Users/GetConnectionsAsString");
            httppost2.setEntity(new UrlEncodedFormEntity(data2));
            HttpResponse httpResponse2 = httpclient2.execute(httppost2);
            HttpEntity entity2 = httpResponse2.getEntity();
            is2 = entity2.getContent();
            StringWriter writer2 = new StringWriter();
            IOUtils.copy(is2, writer2, "UTF-8");
            String theString2 = writer2.toString();
            if (ID >= 0) {
                Intent intent = new Intent(this, MainPage.class);
                bundle = new Bundle();
                bundle.putString("data", theString2);
                bundle.putInt("id",ID);
                intent.putExtras(bundle);
                startActivity(intent);
            } else {
                Toast.makeText(getApplicationContext(), "Wrong e-mail or password",
                        Toast.LENGTH_LONG).show();
            }


                /*BufferedReader streamReader = new BufferedReader(new InputStreamReader(is2, "UTF-8"));
                StringBuilder responseStrBuilder = new StringBuilder();

                String inputStr;
                while ((inputStr = streamReader.readLine()) != null)
                    responseStrBuilder.append(inputStr);
                JSONObject json = new JSONObject(responseStrBuilder.toString());
                ArrayList<String> stringArray = new ArrayList<String>();
                JSONArray jsonArray = null;
                if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.KITKAT) {
                    jsonArray = new JSONArray(json);
                }
                for (int i = 0; i < jsonArray.length(); i++) {
                    stringArray.add(jsonArray.getString(i));
                }
                if (ID >= 0) {
                    Intent intent = new Intent(this, MainPage.class);
                    //intent.putExtras(args);
                    startActivity(intent);
                }
                else{
                    Toast.makeText(getApplicationContext(), "Wrong e-mail or password",
                            Toast.LENGTH_LONG).show();
                }*/

            /*if (userType == "applicant") {
                Log.e("log2", "first connection done, into applicant");
                List<NameValuePair> data2 = new ArrayList<>();
                data2.add(new BasicNameValuePair("userId", id));
                HttpClient httpclient2 = new DefaultHttpClient();
                HttpPost httppost2 = new HttpPost("http://handshake.azurewebsites.net/Users/GetHandshakesForRecruiter");
                httppost2.setEntity(new UrlEncodedFormEntity(data2));
                HttpResponse httpResponse2 = httpclient2.execute(httppost2);
                HttpEntity entity2 = httpResponse2.getEntity();
                is2 = entity2.getContent();
                StringWriter writer2 = new StringWriter();
                IOUtils.copy(is2, writer2, "UTF-8");
                String theString2 = writer2.toString();
                if (ID >= 0) {
                    Intent intent = new Intent(this, MainPage.class);
                    bundle = new Bundle();
                    bundle.putString("data", theString2);
                    intent.putExtras(bundle);
                    startActivity(intent);
                } else {
                    Toast.makeText(getApplicationContext(), "Wrong e-mail or password",
                            Toast.LENGTH_LONG).show();
                }
                /*BufferedReader streamReader = new BufferedReader(new InputStreamReader(is2, "UTF-8"));
                StringBuilder responseStrBuilder = new StringBuilder();

                String inputStr;
                while ((inputStr = streamReader.readLine()) != null)
                    responseStrBuilder.append(inputStr);
                JSONObject json = new JSONObject(responseStrBuilder.toString());
                ArrayList<String> stringArray = new ArrayList<String>();
                JSONArray jsonArray = null;
                if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.KITKAT) {
                    jsonArray = new JSONArray(json);
                }
                for (int i = 0; i < jsonArray.length(); i++) {
                    stringArray.add(jsonArray.getString(i));*/

                //}


                //ArrayList<String> list = new ArrayList<String>();
                //JSONArray jsonArray = (JSONArray)json ;
                //if (jsonArray != null) {
                //    int len = jsonArray.length();
                //    for (int i=0;i<len;i++){
                //        list.add(jsonArray.get(i).toString());
                //    }
                //}

        }
        catch(Exception e) {
            Log.e("log_tag4", e.toString());
        }
    }
}
