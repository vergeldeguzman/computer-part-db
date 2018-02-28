﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ComputerPartsInventory
{
    class DbUtils
    {
        public static List<Part> SelectParts()
        {
            List<Part> parts = new List<Part>();

            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Description, Condition FROM ComputerPart;";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Part part = new Part
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Condition = reader.GetString(2)
                    };
                    parts.Add(part);
                }
            }
            return parts;
        }

        public static PartDetail SelectPartDetail(int id)
        {
            PartDetail part = new PartDetail();

            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = 
                    "SELECT Description,Condition,PartType,Location,Price,Remarks " +
                    "FROM ComputerPart " +
                    $"WHERE Id = {id};";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    part.Description = reader.GetString(0);
                    part.Condition = reader.GetString(1);
                    part.PartType = reader.GetString(2);
                    part.Location = reader.GetString(3);
                    part.Price = reader.GetDecimal(4);
                    part.Remarks = reader.GetString(5);
                }
            }
            return part;
        }

        public static int InsertPart(string description, string condition, string partType,
            string location, decimal price, string remarks)
        {
            // checks on required db fields
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "description");
            }
            if (String.IsNullOrEmpty(condition))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "condition");
            }
            if (String.IsNullOrEmpty(partType))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "partType");
            }

            int primaryKey = -1;
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                    "INSERT INTO ComputerPart (Description,Condition,PartType,Location,Price,Remarks) " +
                    "OUTPUT INSERTED.ID " +
                    $"VALUES (@description,@condition,@partType,@location,{price},@remarks);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@condition", condition);
                command.Parameters.AddWithValue("@partType", partType);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@remarks", remarks);

                connection.Open();
                primaryKey = Convert.ToInt32(command.ExecuteScalar());
            }
            return primaryKey;
        }

        public static void UpdatePart(int id, string description, string condition,
            string partType, string location, decimal price, string remarks)
        {
            // checks on required db fields
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "description");
            }
            if (String.IsNullOrEmpty(condition))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "condition");
            }
            if (String.IsNullOrEmpty(partType))
            {
                throw new ArgumentException("Parameter cannot be null or empty", "partType");
            }

            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                    "UPDATE ComputerPart SET " +
                    $"Description=@description," +
                    $"Condition=@condition," +
                    $"PartType=@partType," +
                    $"Location=@location," +
                    $"Price={price}," +
                    $"Remarks=@remarks " +
                    $"WHERE id={id};";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@condition", condition);
                command.Parameters.AddWithValue("@partType", partType);
                command.Parameters.AddWithValue("@location", location);
                command.Parameters.AddWithValue("@remarks", remarks);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static void DeletePart(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM ComputerPart WHERE id = {id};";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
