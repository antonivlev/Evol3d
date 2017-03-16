﻿using MathNet.Numerics.LinearAlgebra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNN {
	private int num_neurons;

	public Matrix<float> weights;
	public Vector<float> outputs;

	public Neuron[] neurons;

	public RNN() {
		var M = Matrix<float>.Build;
		var V = Vector<float>.Build;

		num_neurons = 10;

		weights = M.Dense (num_neurons, num_neurons, (i, j) => 3); 

		//Init neurons
		neurons = new Neuron[num_neurons];
		for (int i = 0; i < num_neurons; i++) {
			neurons[i] = new Neuron();
		}

		outputs = V.DenseOfArray(new float[num_neurons]);
	}

	public void Update() {
		for (int i = 0; i < num_neurons; i++) {
			Neuron n = neurons [i];
			outputs [i] = n.GetOutput ();
		}

		Vector<float> sums = weights * outputs;

		for (int i = 0; i < num_neurons; i++) {
			Neuron n = neurons [i];
			float rate = (-n.activity + sums [i])*(1/n.t_const);
			n.SetRate (rate);
		}
	}

	public float[] GetOutputs() {
		float[] outputs_arr = outputs.ToArray ();
		return outputs_arr;
	}

	public float[] GetActivities() {
		float[] activities = new float[num_neurons];
		for (int i = 0; i < num_neurons; i++) {
			Neuron n = neurons [i];
			activities [i] = n.activity;
		}
		return activities;
	}
		
	public void SetNeuronParams(float[,] param_list) {
		for (int i = 0; i < param_list.GetLength(0); i++) {
			neurons [i].SetParams (param_list [i, 0], param_list [i, 1]);
		}
	}
}
