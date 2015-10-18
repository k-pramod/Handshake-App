package com.handshake.tommaso.handshake;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.TextView;
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

import java.io.InputStream;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.List;

public class MainPage extends ActionBarActivity {

    ImageButton imageButton;
    String[] listOfMembers;
    String theString;
    Bundle bundle;
    String id;
    ListView listView;
    //TextView textView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_page);
        imageButton = (ImageButton) findViewById(R.id.imageButton);
        Intent intent = getIntent();
        bundle = intent.getExtras();
        id = String.valueOf(bundle.getInt("id"));
        imageButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                InputStream is2;
                try {
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
                    theString = writer2.toString();
                }
                catch(Exception e){
                    Log.e("log_tag imaeButton","Failed connection");
                }
            }
        });
        if(theString == null) {
            String data = bundle.getString("data");
            listOfMembers = data.split("\\|");
        } else {
            listOfMembers = theString.split("\\|");
        }
        ListView listView = (ListView) findViewById(R.id.listView);

        ArrayAdapter<String> myAdapter=new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, listOfMembers);
        ListView myList= (ListView) findViewById(R.id.listView);
        myList.setAdapter(myAdapter);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view,
                                    int position, long id) {

                String link = (listOfMembers[position]).split(",")[3];
                Toast.makeText(getApplicationContext(), link,
                        Toast.LENGTH_LONG).show();
                showPdf(link);
            }
        });

    }

    private void showPdf(String link) {
        try {
            Uri uri = Uri.parse(link);
            Intent intent = new Intent(Intent.ACTION_VIEW, uri);
            startActivity(intent);
        }
        catch(Exception e){
            Log.e("log_tag", "Failed connection");
        }
    }

}
